using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CahootSOOA.Models;
using System.Drawing.Printing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace CahootSOOA.Controllers
{
    public class PostDTO
    {
        public int PostId { get; set; } // Ensure this matches your query alias
        public string Title { get; set; }
        public string DescriptionPreview { get; set; }
        public int AnswerCount { get; set; }
        public string UserName { get; set; }
        public int Reputation { get; set; }
        public long TotalVotes { get; set; }
        public string Badges { get; set; }
    }
    public class SearchhhViewModel
    {
        public List<PostDTO> Posts { get; set; }
        public int pageNumber { get; set; }
        public string searchQuery { get; set; }
    }
        public class PostsController : Controller
    {
        private readonly StackOverflow2010Context _context;

        public PostsController(StackOverflow2010Context context)
        {
            _context = context;
        }
        // GET: Posts/Search
        public async Task<IActionResult> Search(string searchQuery, int pageNumber, int pageSize)
        {
           
            if (searchQuery == null)
            {
                return View(new SearchhhViewModel { Posts = [], pageNumber = 0 , searchQuery="" });
            }
            var posts = new List<PostDTO>();

            // Create the SQL query with parameters
            var sqlQuery = $@"SELECT
                        p.Id AS PostId,
                        p.Title,
                        LEFT(p.Body, 140) AS DescriptionPreview,
                        p.AnswerCount,
                        u.DisplayName AS UserName,
                        u.Reputation,
                        COUNT_BIG(DISTINCT v.Id) AS TotalVotes,
                        CAST((SELECT STRING_AGG(CAST(B.Name AS NVARCHAR(MAX)), ', ') 
                              FROM Badges B
                              WHERE B.UserId = p.OwnerUserId) AS NVARCHAR(MAX)) AS Badges
                      FROM dbo.posts p
                      LEFT JOIN dbo.Users u ON u.id = p.OwnerUserId
                      LEFT JOIN dbo.Votes v ON v.PostId = p.Id
                      WHERE p.PostTypeId = 1 AND p.OwnerUserId > 0 AND CONTAINS(p.*, @SearchQuery)
                      GROUP BY p.Id, p.Title, LEFT(p.Body, 140), p.AnswerCount, u.DisplayName, u.Reputation, p.OwnerUserId
                      ORDER BY p.Id
                      OFFSET (@PageNumber - 1) * @PageSize ROWS
                      FETCH NEXT @PageSize ROWS ONLY";

            // Ensure you have a using directive for System.Data.SqlClient or Microsoft.Data.SqlClient
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlQuery;
                command.Parameters.Add(new SqlParameter("@SearchQuery", searchQuery));
                command.Parameters.Add(new SqlParameter("@PageNumber", pageNumber+1));
                command.Parameters.Add(new SqlParameter("@PageSize", 10));
        
                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        posts.Add(new PostDTO
                        {
                            PostId = result.GetInt32(result.GetOrdinal("PostId")),
                            Title = result.GetString(result.GetOrdinal("Title")),
                            DescriptionPreview = result.GetString(result.GetOrdinal("DescriptionPreview")),
                            AnswerCount = result.GetInt32(result.GetOrdinal("AnswerCount")),
                            UserName = result.GetString(result.GetOrdinal("UserName")),
                            Reputation = result.GetInt32(result.GetOrdinal("Reputation")),
                            TotalVotes = result.GetInt64(result.GetOrdinal("TotalVotes")),
                            Badges = result.IsDBNull(result.GetOrdinal("Badges")) ? null : result.GetString(result.GetOrdinal("Badges")),
                        });
                    }
                }

                _context.Database.CloseConnection();
            }
            var sendPostsData = new SearchhhViewModel { Posts = posts, pageNumber=pageNumber+1, searchQuery=searchQuery };
            return View(sendPostsData);
        } 
    }
}

