﻿using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interfaces;
using DataAccessLayer.RequestModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Classes
{
	public class StudentRepo : IStudentRepo
	{
		private readonly SoftoneStudentSystemContext dbContext;
		public string ConnectionString { get; set; }

		private int retunVal;
		public StudentRepo(SoftoneStudentSystemContext dbContext)
		{
			this.dbContext = dbContext;  
		}



		public async Task<List<StudentPersonal>> GetStudents(string  constr)
		{
			Initalize(constr);
			if (dbContext.StudentPersonals == null)  //Check Student Null
			{
				throw new Exception("Not Found");
			}
			this.dbContext.ConnectionString=constr;
			return await dbContext.StudentPersonals.ToListAsync();
		}
		public async Task<List<StudentPersonal>> GetStudents(string constr,  Guid Id, string StudentCode)
		{
			try
			{
				Initalize(constr);
				var result = dbContext.StudentPersonals.Where(x => x.Id == Id && x.StudentCode == StudentCode);
				if (result.Any())
				{
					return await result.ToListAsync();
				}
				else
				{
					throw new Exception("No data found");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void Initalize(string constr)
		{
			this.dbContext.ConnectionString = constr;
		}

		public async Task<int> PostStudents(StudentRequest StuRequest, string constr)
		{
			Initalize(constr);
			using (var transaction = dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))  //Begin transaction
			{
				try
				{
					var domainModeStudent = new DataAccessLayer.Models.StudentPersonal
					{
						Id = Guid.NewGuid(),
						StudentCode = StuRequest.StudentCode,
						FirstName = StuRequest.FirstName,
						LastName = StuRequest.LastName,
						Mobile = StuRequest.Mobile,
						Email = StuRequest.Email,
						Nic = StuRequest.NIC,
						Dob = StuRequest.Dob,
						Address = StuRequest.Address
					};

					dbContext.StudentPersonals.Add(domainModeStudent);
					await dbContext.SaveChangesAsync();
					transaction.Commit();   //Commit  transaction
					retunVal = 1;
					return retunVal;
					//return CreatedAtAction(nameof(GetStudent), new { Id = StuRequest.Id }, StuRequest); //Redirect to Student get method 
				}
				catch (Exception ex)
				{
					transaction.Rollback();  //Rollback transaction if any exeception 

					throw new Exception(ex.Message);
				}
			}
		}

		public async Task<int> PostStudents(Guid Id, string StudentCode, StudentRequest StuRequest, string constr)
		{

			Initalize(constr);
			using (var transaction = dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))  //Begin transaction
			{
				try
				{

					var stu = await dbContext.StudentPersonals.FindAsync(Id, StudentCode);
					if (stu != null)
					{
						stu.FirstName = StuRequest.FirstName;
						stu.LastName = StuRequest.LastName;
						stu.StudentCode = StuRequest.StudentCode;
						stu.Email = StuRequest.Email;
						stu.Mobile = StuRequest.Mobile;
						stu.Nic = StuRequest.NIC;
						stu.Address = StuRequest.Address;
						stu.Dob = StuRequest.Dob;

					}
					await dbContext.SaveChangesAsync();
					transaction.Commit();
					retunVal = 1;
					return retunVal;

				}
				catch (Exception ex)
				{
					transaction.Rollback();  //Rollback transaction if any exeception 

					throw new Exception(ex.Message);
				}
			}
		}

		public async Task<int> DeleteStudent(Guid Id, string StudentCode, string constr)
		{
			try
			{
				Initalize(constr);
				var stu = await dbContext.StudentPersonals.FindAsync(Id, StudentCode);
				dbContext.StudentPersonals.Remove(stu);  //Remove the student 
				await dbContext.SaveChangesAsync(); //Update the database
				retunVal = 1;

				return retunVal;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
