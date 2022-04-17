using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthPet.Models;
using Microsoft.Data.SqlClient;
using HealthPet.Email;

namespace HealthPet.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HealthPetDBContext _context;

        public AppointmentsController(HealthPetDBContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Now;
            var list = await _context.Appointments.Where(x => x.Date >= today).OrderBy(x => x.Date).ThenBy(x => x.Shift).ToListAsync();
            return View(list);
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id, bool? sent)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // Get Available shifts by date
        public IActionResult GetShifts(DateTime date)
        {
            var shifts = (from a in _context.Appointments
                         where a.Date >= date
                          && a.Date < date.Date.AddDays(1)
                          select a.Shift).ToList();

            if (shifts == null)
            {
                return NotFound();
            }

            return Json(new
            {
                list = shifts
            });
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Categorie,Date,Shift,FirstName,LastName,IdCard,Phone,Email,PetName,PetType,PetAge,Race")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Date = appointment.Date.AddMinutes((appointment.Shift + 15) * 30);
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", new { id = appointment.Id });
            }
            return View(appointment);
            
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Categorie,Date,Shift,Available,FirstName,LastName,IdCard,Phone,Email,PetName,PetType,PetAge,Race")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appointment.Date = appointment.Date.AddMinutes((appointment.Shift + 15) * 30);
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = appointment.Id });
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var appointment = await _context.Appointments
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(appointment);
        //}

        // POST: Appointments/Delete/5

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Print(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmail(appointment);

            //return Ok(appointment);
            return RedirectToAction("Details", new { id = appointment.Id, send = true });
        }
    }
}
