
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentSystemMvcCore.DBModel;
using StudentSystemMvcCore.Models;


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
      
        public async Task<ActionResult> Details(Guid? Id)
        {
            if (Id == null || stuContext.StudentPersonals == null)
            {
                return NotFound();
            }
            var student = await stuContext.StudentPersonals
           .FirstOrDefaultAsync(m => m.Id == Id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);

        }

        public ActionResult Create()
        {

            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("StudentCode,FirstName,LastName,Mobile,Email,Nic,Dob,Address")] StudentPersonal stuPersonalRq)
        {
            try
            {
              
                if (ModelState.IsValid)
                {
                    stuContext.Add(stuPersonalRq);
                    await stuContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                   
                }
                else
                {
                    return View(stuPersonalRq);
                }
             
               
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null || stuContext.StudentPersonals == null)
            {
                return NotFound();
            }
            var student = await stuContext.StudentPersonals.FirstOrDefaultAsync(x=>x.Id==id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
          
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id,[Bind("Id,StudentCode,FirstName,LastName,Mobile,Email,Nic,Dob,Address")] StudentPersonal stuPersonal)
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
                        if (!StudentExists(id))
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
        public async Task<ActionResult> Delete(Guid id)
        {
            if (stuContext.StudentPersonals == null)
            {
                return NotFound();
            }
            var student = await stuContext.StudentPersonals.FirstOrDefaultAsync(x => x.Id == id );
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync(Guid id)
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

        private bool StudentExists(Guid id)
        {
            return (stuContext.StudentPersonals?.Any(e => e.Id == id )).GetValueOrDefault();
        }
    }
}
