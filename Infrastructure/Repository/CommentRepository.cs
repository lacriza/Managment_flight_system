using Core.POCO;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
  public class CommentRepository : BaseRepo, IRepository<Comment>
  {
    public CommentRepository(IConfiguration config) : base(config)
    {
    }

    public async Task<IEnumerable<Comment>> GetAll()
    {
      List<Comment> comments = new List<Comment>();

      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand(
          "SELECT * FROM Comments", connection);

        SqlDataReader rdr = command.ExecuteReader();
        while (await rdr.ReadAsync())
        {
          var comment = new Comment();
          comment.CommentId = Convert.ToInt32(rdr["commentId"]);
          comment.FlightType = (FlightType)Convert.ToInt32(rdr["flightType"]);
          comment.Text = rdr["comment"].ToString();
          comments.Add(comment);
        }
        return comments;
      }
    }

    public async Task Insert(Comment entity)
    {
      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand("stpInsertComment", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@commentId", entity.CommentId));
        command.Parameters.Add(new SqlParameter("@flightType", entity.FlightType));
        command.Parameters.Add(new SqlParameter("@comment", entity.Text));
        await command.ExecuteNonQueryAsync();
      }
    }

    public async Task<Comment> GetById(string id)
    {
      int idDb = Convert.ToInt32(id);

      Comment comment = new Comment();
      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand("stpGetCommentById", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@commentId", idDb));

        SqlDataReader rdr = command.ExecuteReader();
        while (await rdr.ReadAsync())
        {
          comment.Text = rdr["comment"].ToString();
          comment.FlightType = (FlightType)Convert.ToInt32(rdr["flightType"]);
          comment.CommentId = Convert.ToInt32(rdr["commentId"]);
        }
        return comment;
      }
    }

    public async Task Update(Comment entity)
    {
      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand("stpUpdateComment", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@commentId", entity.CommentId));
        command.Parameters.Add(new SqlParameter("@flightType", entity.FlightType));
        command.Parameters.Add(new SqlParameter("@comment", entity.Text));
        await command.ExecuteNonQueryAsync();
      }
    }

    public async Task Delete(string id)
    {
      int idDb = Convert.ToInt32(id);
      using (var connection = GetOpenConnection())
      {
        SqlCommand command = new SqlCommand("stpDeleteComment", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@commentId", idDb));

        await command.ExecuteNonQueryAsync();
      }
    }
  }
}