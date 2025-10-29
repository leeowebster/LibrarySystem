﻿using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibrarySystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        // GET: api/<ReportController>/BorrowedBooks
        [HttpGet("BorrowedBooks")]
        public ActionResult<IEnumerable<(People, List<Borrow>)>> Get()
        {
            var borrowedBooks = _reportService.ActiveBorrows();
            return Ok(borrowedBooks);
        }


        // GET api/<ReportController>/DelayedBorrows
        [HttpGet("DelayedBorrows")]
        public ActionResult<IEnumerable<Borrow>> GetDelayedBorrows()
        {
            var delayedBorrows = _reportService.DelayedBorrows();
            return Ok(delayedBorrows);
        }

    }
}
