using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
using PagedList.Mvc;
using PagedList;
namespace Web_Shop_Zay.Areas.Admin.Controllers
{
    public class PhanLoaiSPController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: Admin/PhanLoaiSP
        public ActionResult Index(int? page)
        {

            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var phanLoai = (from l in db.Phan_Loai_SP
                         select l).OrderBy(x => x.MaPL);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 3;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(phanLoai.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddPhanLoaiSP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPhanLoaiSP(Phan_Loai_SP phanLoai)
        {
            var ds = db.Phan_Loai_SP.SingleOrDefault(m => m.MaPL == phanLoai.MaPL);
            if (ds == null | phanLoai.MaLoai !=null | phanLoai.TenPL != null)
            {
                db.Phan_Loai_SP.Add(phanLoai);
                db.SaveChanges();

            }
            else
            {
                Session["Erorr"] = "Trùng mã phân loại vui lòng nhập mã khác!";
                Session["Null"] = "Mời bạn nhập đầy đủ thông tin!";
                return RedirectToAction("AddPhanLoaiSP");
            }
            Session["ThongBao"] = "Đã thêm thành công!";
            return RedirectToAction("index");
        }


        public ActionResult EditPhanLoai(int id)
        {
            var ds = db.Phan_Loai_SP.SingleOrDefault(m => m.MaPL == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult EditPhanLoai(Phan_Loai_SP phan_Loai_SP)
        {
            var ds = db.Phan_Loai_SP.SingleOrDefault(m => m.MaPL == phan_Loai_SP.MaPL);
            if( phan_Loai_SP.MaLoai != null || phan_Loai_SP.TenPL != null) {
                ds.MaPL = phan_Loai_SP.MaPL;
                ds.MaLoai = phan_Loai_SP.MaLoai;
                ds.TenPL = phan_Loai_SP.TenPL;
                db.SaveChanges();
                TempData["ThongBao"] = "Đã sửa thành công!";
            }
            else
            {
                Session["Erorr"] = "Trùng mã phân loại vui lòng nhập mã khác!";
                Session["Null"] = "Mời bạn nhập đầy đủ thông tin!";
                return RedirectToAction("AddPhanLoaiSP");
            }
            return RedirectToAction("index");

        }
        public ActionResult DeletePhanLoaiSP(int id)
        {
            var ds = db.Phan_Loai_SP.SingleOrDefault(m => m.MaPL == id);
            db.Phan_Loai_SP.Remove(ds);
            db.SaveChanges();
            TempData["ThongBao"] = "Đã xóa thành công!";
            return RedirectToAction("index");
        }
    }
}