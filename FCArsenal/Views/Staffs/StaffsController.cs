using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FCArsenal.Data;
using FCArsenal.Models;
using FCArsenal.Models.FootballViewModels;


namespace FCArsenal.Views.Staffs
{
    public class StaffsController : Controller
    {
        private readonly FootballContext _context;

        public StaffsController(FootballContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index(int? id, int? trainingID)
        {
            var viewModel = new StaffIndexData();
            viewModel.Staffs = await _context.Staffs
                  .Include(i => i.OfficeAssignment)
                  .Include(i => i.TrainingAssignments)
                    .ThenInclude(i => i.Training)
                        .ThenInclude(i => i.Signings)
                            .ThenInclude(i => i.Player)
                  .Include(i => i.TrainingAssignments)
                    .ThenInclude(i => i.Training)
                        .ThenInclude(i => i.Department)
                  .AsNoTracking()
                  .OrderBy(i => i.LastName)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["StaffID"] = id.Value;
                Staff staff = viewModel.Staffs.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Trainings = staff.TrainingAssignments.Select(s => s.Training);
            }

            if (trainingID != null)
            {
                ViewData["TrainingID"] = trainingID.Value;
                viewModel.Signings = viewModel.Trainings.Where(
                    x => x.TrainingID == trainingID).Single().Signings;
            }

            return View(viewModel);
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,HireDate")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
        .Include(i => i.OfficeAssignment)
        .Include(i => i.TrainingAssignments).ThenInclude(i => i.Training)
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.ID == id);

            if (staff == null)
            {
                return NotFound();
            }
            PopulateAssignedTrainingData(staff);
            return View(staff);
        }
        private void PopulateAssignedTrainingData(Staff staff)
        {
            var allTrainings = _context.Trainings;
            var staffTrainings = new HashSet<int>(staff.TrainingAssignments.Select(c => c.TrainingID));
            var viewModel = new List<AssignedTrainingData>();
            foreach (var training in allTrainings)
            {
                viewModel.Add(new AssignedTrainingData
                {
                    TrainingID = training.TrainingID,
                    Title = training.Title,
                    Assigned = staffTrainings.Contains(training.TrainingID)
                });
            }
            ViewData["Trainings"] = viewModel;
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedTrainings)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffToUpdate = await _context.Staffs
                .Include(i => i.OfficeAssignment)
                .Include(i => i.TrainingAssignments)
            .ThenInclude(i => i.Training)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<Staff>(
                staffToUpdate,
                "",
                i => i.FirstMidName, i => i.LastName, i => i.HireDate, i => i.OfficeAssignment))
            {
                if (String.IsNullOrWhiteSpace(staffToUpdate.OfficeAssignment?.Location))
                {
                    staffToUpdate.OfficeAssignment = null;
                }
                UpdateStaffTrainings(selectedTrainings, staffToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateStaffTrainings(selectedTrainings, staffToUpdate);
            PopulateAssignedTrainingData(staffToUpdate);
            return View(staffToUpdate);
        }

        private void UpdateStaffTrainings(string[] selectedTrainings, Staff staffToUpdate)
        {
            if (selectedTrainings == null)
            {
                staffToUpdate.TrainingAssignments = new List<TrainingAssignment>();
                return;
            }

            var selectedTrainingsHS = new HashSet<string>(selectedTrainings);
            var staffTrainings = new HashSet<int>
                (staffToUpdate.TrainingAssignments.Select(c => c.Training.TrainingID));
            foreach (var training in _context.Trainings)
            {
                if (selectedTrainingsHS.Contains(training.TrainingID.ToString()))
                {
                    if (!staffTrainings.Contains(training.TrainingID))
                    {
                        staffToUpdate.TrainingAssignments.Add(new TrainingAssignment { StaffID = staffToUpdate.ID, TrainingID = training.TrainingID });
                    }
                }
                else
                {

                    if (staffTrainings.Contains(training.TrainingID))
                    {
                        TrainingAssignment trainingToRemove = staffToUpdate.TrainingAssignments.FirstOrDefault(i => i.TrainingID == training.TrainingID);
                        _context.Remove(trainingToRemove);
                    }
                }
            }
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(e => e.ID == id);
        }
    }
}
