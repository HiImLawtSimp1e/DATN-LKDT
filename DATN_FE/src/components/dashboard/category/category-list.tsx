"use client";

import { deleteCategory } from "@/action/categoryAction";
import Pagination from "@/components/ui/pagination";
import TagFiled from "@/components/ui/tag";
import { useCustomActionState } from "@/lib/custom/customHook";
import { formatDate } from "@/lib/format/format";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { MdAdd } from "react-icons/md";
import { toast } from "react-toastify";

interface IProps {
  categories: ICategory[];
  pages: number;
  currentPage: number;
}

const CategoryList = ({ categories, pages, currentPage }: IProps) => {
  //using for pagination
  const pageSize = 10;
  const startIndex = (currentPage - 1) * pageSize;

  //using for delete action
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    deleteCategory,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!window.confirm("Bạn có chắc chắn muốn xóa danh mục này không?")) {
      return;
    }
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Reset toastDisplayed when submitting
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error("Xóa danh mục thất bại!");
      setToastDisplayed(true); // Set toastDisplayed to true to prevent multiple toasts
    }
    if (formState.success) {
      toast.success("Xóa danh mục thành công!");
      router.push("/dashboard/category");
    }
  }, [formState, toastDisplayed]);

  return (
    <div className="bg-gray-800 p-5 rounded-lg mt-5">
      <div className="flex items-center justify-end mb-5">
        <Link href="/dashboard/category/add">
          <button className="p-2 flex items-center justify-center mb-5 bg-purple-600 text-white rounded">
            <MdAdd />
            Thêm Danh Mục Mới
          </button>
        </Link>
      </div>
      <table className="w-full text-left text-gray-400">
        <thead className="bg-gray-700 text-gray-400 uppercase">
          <tr>
            <th className="px-4 py-2">#</th>
            <th className="px-4 py-2">Tiêu đề</th>
            <th className="px-4 py-2">Slug</th>
            <th className="px-4 py-2">Trạng thái</th>
            <th className="px-4 py-2">Ngày tạo</th>
            <th className="px-4 py-2">Ngày sửa</th>
            <th className="px-4 py-2">Người tạo</th>
            <th className="px-4 py-2">Người sửa</th>
            <th className="px-4 py-2"></th>
          </tr>
        </thead>
        <tbody>
          {categories.map((category: ICategory, index) => (
            <tr key={category.id} className="border-b border-gray-700">
              <td className="px-4 py-2">{startIndex + index + 1}</td>
              <td className="px-4 py-2">{category.title}</td>
              <td className="px-4 py-2">{category.slug}</td>
              <td className="px-4 py-2">
                <TagFiled
                  cssClass={category.isActive ? "bg-lime-900" : "bg-red-700"}
                  context={category.isActive ? "Hoạt động" : "Ngưng hoạt động"}
                />
              </td>
              <td className="px-4 py-2">{formatDate(category.createdAt)}</td>
              <td className="px-4 py-2">{formatDate(category.modifiedAt)}</td>
              <td className="px-4 py-2">{category.createdBy}</td>
              <td className="px-4 py-2">{category.modifiedBy}</td>
              <td className="px-4 py-2">
                <div className="flex gap-2">
                  <Link href={`/dashboard/category/${category.id}`}>
                    <button className="m-1 px-5 py-2 bg-teal-500 text-white rounded">
                      Xem
                    </button>
                  </Link>
                  <form onSubmit={handleSubmit}>
                    <input type="hidden" name="id" value={category.id} />
                    <button className="m-1 px-5 py-2 bg-red-500 text-white rounded">
                      Xóa
                    </button>
                  </form>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Pagination
        pages={pages}
        currentPage={currentPage}
        pageSize={pageSize}
        clsColor="gray"
      />
    </div>
  );
};

export default CategoryList;
