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
        SqlCommand command = new SqlCommand(
          "INSERT INTO Comments VALUES (@id, @flightType, @comment)", connection);

        command.Parameters.Add(new SqlParameter("@id", entity.CommentId));
        command.Parameters.Add(new SqlParameter("@flightType", entity.FlightType));
        command.Parameters.Add(new SqlParameter("@comment", entity.Text));
        command.CommandType = CommandType.Text;
        await command.ExecuteNonQueryAsync();
      }
    }

    public Task<Comment> GetById(string id)
    {
      throw new NotImplementedException();
    }

    public Task Update(Comment entity)
    {
      throw new NotImplementedException();
    }
  }
}