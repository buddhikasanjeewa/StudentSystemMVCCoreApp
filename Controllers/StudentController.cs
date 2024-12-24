using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentSystemMvcCore.DBModel;

namespace StudentSystemMvcCore.Controllers
{
    public class StudentController : Controller
    {
        private readonly GitstudentContext stuContext;

        public StudentController(GitstudentContext _stuContext)
        {
            this.stuContext = _stuContext;
        }
        // GET: StudentController
        public async Task<ActionResult> Index()
        {
           var result =await stuContext.StudentPersonals.ToListAsync();
            return View(result);
        }

        // GET: StudentController/Details/5
        public async Task<ActionResult> Details(Guid? id,string studentcode)
        {
            if (id == null || stuContext.StudentPersonals == null)
            {
                return NotFound();
            }
            var student = await stuContext.StudentPersonals
           .FirstOrDefaultAsync(m => m.Id==id && m.StudentCode==studentcode);
            if (student== null)
            {
                return NotFound();
            }
            return View(student);
      
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("StudentCode,FirstName,LastName,Mobile,Email,NIC,DOB")] StudentPersonal stuPersonal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stuContext.Add(stuPersonal);
                    await stuContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            
                return View(stuPersonal);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(Guid? id, string studentcode)
        {
            if (id == null || stuContext.StudentPersonals == null)
            {
                return NotFound();
            }
            var student = await stuContext.StudentPersonals.FirstOrDefaultAsync(x=>x.Id==id && x.StudentCode==studentcode);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
          
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, string studentcode,[Bind("StudentCode,FirstName,LastName,Mobile,Email,NIC,DOB")] StudentPersonal stuPersonal)
        {
            try
            {
                if ( stuContext.StudentPersonals == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        stuContext.Update(stuPersonal);
                        await stuContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StudentExists(id,studentcode))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }

              
                return View(stuPersonal);
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(Guid id, string studentcode)
        {
            if (stuContext.StudentPersonals == null)
            {
                return NotFound();
            }
            var student = await stuContext.StudentPersonals.FirstOrDefaultAsync(x => x.Id == id && x.StudentCode == studentcode);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync(Guid id, string studentcode)
        {
            try
            {
                if (stuContext.StudentPersonals == null)
                {
                    return Problem("Entity set 'EFCoreDbContext.Student' is null.");
                }
                var student = await stuContext.StudentPersonals.FindAsync(id);
                if (student != null)
                {
                    stuContext.StudentPersonals.Remove(student);
                }

                await stuContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool StudentExists(Guid id,string studentcode)
        {
            return (stuContext.StudentPersonals?.Any(e => e.Id == id && e.StudentCode==studentcode)).GetValueOrDefault();
        }
    }
}
