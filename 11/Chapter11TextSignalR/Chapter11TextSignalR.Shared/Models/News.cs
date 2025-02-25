using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter11TextSignalR.Shared.Models;


public record Article(Source source, string author, string title, string description, string url, string urlToImage, DateTime publishedAt, string content);

public record Root(string status, int totalResults, List<Article> articles);

public record Source(string id, string name);
