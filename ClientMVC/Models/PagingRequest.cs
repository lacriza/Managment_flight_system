﻿namespace ClientMVC.Models
{
  public class PagingRequest
  {
    private int _pageNumber = 1;
    private int _pageSize = 5;

    public int PageNumber { get => _pageNumber; set => _pageNumber = value; }
    public int PageSize { get => _pageSize; set => _pageSize = value; }
  }
}