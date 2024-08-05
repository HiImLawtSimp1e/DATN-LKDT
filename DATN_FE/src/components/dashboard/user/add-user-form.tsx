"use client";

import React, { useEffect, useState } from "react";
import InputField from "@/components/ui/input";
import { createUser } from "@/action/userAction";
import { useCustomActionState } from "@/lib/custom/customHook";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";

interface IProps {
  roleSelect: IRole[];
}

const AddUserForm = ({ roleSelect }: IProps) => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    createUser,
    initialState
  );

  const [formData, setFormData] = useState({
    username: "",
    password: "",
    name: "",
    email: "",
    phoneNumber: "",
    address: "",
    roleId: "",
  });

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Đặt lại toastDisplayed khi đang submit
  };

  const handleChange = (
    e:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: value,
    }));
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error("Tạo người dùng thất bại");
      setToastDisplayed(true); // Đặt toastDisplayed là true để tránh hiển thị nhiều toast
    }
    if (formState.success) {
      toast.success("Tạo người dùng thành công!");
      router.push("/dashboard/users");
    }
  }, [formState, toastDisplayed]);

  return (
    <form onSubmit={handleSubmit} className="px-4 w-full">
      <InputField
        label="Tên đăng nhập"
        id="username"
        name="username"
        value={formData.username}
        onChange={handleChange}
        required
      />
      <InputField
        label="Email"
        id="email"
        name="email"
        type="email"
        value={formData.email}
        onChange={handleChange}
        required
      />
      <InputField
        label="Mật khẩu"
        id="password"
        name="password"
        type="password"
        value={formData.password}
        onChange={handleChange}
        required
      />
      <InputField
        label="Họ và tên"
        id="name"
        name="name"
        value={formData.name}
        onChange={handleChange}
        required
      />
      <InputField
        label="Số điện thoại"
        id="phoneNumber"
        name="phoneNumber"
        value={formData.phoneNumber}
        onChange={handleChange}
        required
      />
      <InputField
        label="Địa chỉ"
        id="address"
        name="address"
        value={formData.address}
        onChange={handleChange}
        required
      />
      <label className="block mb-2 text-sm font-medium text-white">
        Vai trò
      </label>
      <select
        name="roleId"
        value={formData.roleId}
        onChange={handleChange}
        className="text-sm rounded-lg w-full p-2.5 bg-gray-600 placeholder-gray-400 text-white"
      >
        {roleSelect?.map((role: IRole, index) => (
          <option key={index} value={role.id}>
            {role.roleName === "Customer"
              ? "Khách hàng"
              : role.roleName === "Employee"
              ? "Nhân viên"
              : role.roleName === "Admin"
              ? "Admin"
              : ""}
          </option>
        ))}
      </select>
      {formState.errors.length > 0 && (
        <ul>
          {formState.errors.map((error, index) => (
            <li className="text-red-400" key={index}>
              {error}
            </li>
          ))}
        </ul>
      )}
      <button
        type="submit"
        className="float-right mt-4 text-white bg-blue-700 hover:bg-blue-800 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center"
      >
        Tạo mới
      </button>
    </form>
  );
};

export default AddUserForm;
