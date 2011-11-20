using System.Linq;
using System.Web.Mvc;
using Kreissl.Showcase.Model;

namespace Kreissl.Showcase.MvcFrontend.Controllers
{
    using Kreissl.Showcase.Data.Interfaces;

    using Kreissl.Showcase.Repository.Interfaces;

    public class BookController : Controller
    {
        public BookController(IUnitOfWork unitOfWork, IBookRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        private readonly IUnitOfWork _unitOfWork;

        private IBookRepository _repository;

        public ViewResult Index()
        {
            return View(_repository.GetAll().ToList());
        }

        public ViewResult Details(int id)
        {
            Book book = _repository.GetById(id);
            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(book);
                _unitOfWork.Commit();
                return RedirectToAction("Index");  
            }

            return View(book);
        }
        
        public ActionResult Edit(int id)
        {
            Book book = _repository.GetById(id);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(book);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult Delete(int id)
        {
            Book book = _repository.GetById(id);
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            _repository.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}