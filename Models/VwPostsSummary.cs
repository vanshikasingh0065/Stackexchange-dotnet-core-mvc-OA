using System;
using System.Collections.Generic;

namespace CahootSOOA.Models;

public partial class VwPostsSummary
{
    public int PostId { get; set; }

    public string? Title { get; set; }

    public int? AnswerCount { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public int Reputation { get; set; }

    public long? TotalVotes { get; set; }

    public string? Bage { get; set; }
}
