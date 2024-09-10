"use client";

import { deleteUser } from "@/action/userAction";
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
  users: IUser[];
  pages: number;
  currentPage: number;
}

const UserList = ({ users, pages, currentPage }: IProps) => {
  // Sử dụng cho phân trang
  const pageSize = 10;
  const startIndex = (currentPage - 1) * pageSize;

  // Sử dụng cho hành động xóa
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    deleteUser,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!window.confirm("Bạn có chắc muốn xóa người dùng này?")) {
      return;
    }
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Đặt lại toastDisplayed khi đang submit
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error(formState.errors[0]);
      setToastDisplayed(true); // Đặt toastDisplayed là true để tránh hiển thị nhiều toast
    }
    if (formState.success) {
      toast.success("Xóa người dùng thành công!");
      router.push("/dashboard/users");
    }
  }, [formState, toastDisplayed]);

  return (
    <div className="bg-gray-800 p-5 rounded-lg mt-5">
      <div className="flex items-center justify-end mb-5">
        <Link href="/dashboard/users/add">
          <button className="p-2 flex items-center justify-center mb-5 bg-purple-600 text-white rounded">
            <MdAdd />
            Tạo Tài Khoản Mới
          </button>
        </Link>
      </div>
      <table className="w-full text-left text-gray-400">
        <thead className="bg-gray-700 text-gray-400 uppercase">
          <tr>
            <th className="px-4 py-2">#</th>
            <th className="px-4 py-2">Tên tài khoản</th>
            <th className="px-4 py-2">Vai trò</th>
            <th className="px-4 py-2">Trạng thái</th>
            <th className="px-4 py-2">Ngày tạo</th>
            <th className="px-4 py-2">Ngày chỉnh sửa</th>
            <th className="px-4 py-2">Hành động</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user: IUser, index) => (
            <tr key={user.id} className="border-b border-gray-700">
              <td className="px-4 py-2">{startIndex + index + 1}</td>
              <td className="px-4 py-2">{user.username}</td>
              <td className="px-4 py-2">
                <TagFiled
                  cssClass={
                    user.role.roleName === "Admin"
                      ? "bg-slate-700"
                      : user.role.roleName === "Customer"
                      ? "bg-blue-700"
                      : "bg-violet-700"
                  }
                  context={
                    user.role.roleName === "Customer"
                      ? "Khách hàng"
                      : user.role.roleName === "Employee"
                      ? "Nhân viên"
                      : user.role.roleName === "Admin"
                      ? "Admin"
                      : ""
                  }
                />
              </td>
              <td className="px-4 py-2">
                <TagFiled
                  cssClass={user.isActive ? "bg-lime-900" : "bg-red-700"}
                  context={user.isActive ? "Hoạt động" : "Ngưng hoạt động"}
                />
              </td>
              <td className="px-4 py-2">
                {formatDate(user.createdAt?.toString() || "")}
              </td>
              <td className="px-4 py-2">
                {formatDate(user.modifiedAt?.toString() || "")}
              </td>
              <td className="px-4 py-2">
                <div className="flex gap-2">
                  <Link href={`/dashboard/users/${user.id}`}>
                    <button className="m-1 px-5 py-2 bg-teal-500 text-white rounded">
                      Chi Tiết
                    </button>
                  </Link>
                  <form onSubmit={handleSubmit}>
                    <input type="hidden" name="id" value={user.id} />
                    <button
                      className={`m-1 px-5 py-2 bg-red-500 text-white rounded ${
                        user.role.roleName === "Admin"
                          ? "opacity-50 cursor-not-allowed"
                          : "cursor-pointer"
                      }`}
                      disabled={user.role.roleName === "Admin"}
                    >
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
export default UserList;
