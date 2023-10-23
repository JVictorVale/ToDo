﻿using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Interfaces;

public interface IPagedResult<T> where T : BaseEntity, new()
{
    public List<T> Items { get; set; }
    public int Total { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int PageCount { get; set; }
}