"use client";

import { changePasswordAction } from "@/action/accountAction";
import { useCustomActionState } from "@/lib/custom/customHook";
import { setLogoutPublic } from "@/service/auth-service/auth-service";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

const ChangePassword = () => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    changePasswordAction,
    initialState
  );

  const [toastDisplayed, setToastDisplayed] = useState(false);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    formAction(formData);
    setToastDisplayed(false); // Reset toastDisplayed when submitting
  };

  useEffect(() => {
    if (formState.errors.length > 0 && !toastDisplayed) {
      toast.error("Đổi mật khẩu thất bại!");
      setToastDisplayed(true); // Set toastDisplayed to true to prevent multiple toasts
    }
    if (formState.success) {
      toast.success("Đổi mật khẩu thành công, đăng nhập lại với mật khẩu mới!");
      setLogoutPublic();
      router.push("/profile");
    }
  }, [formState, toastDisplayed]);

  return (
    <div className="flex min-h-[100vh]">
      {/* Left Pane */}
      <div className="w-full bg-gray-100 flex items-center justify-center">
        <div className="max-w-md w-full p-6">
          <h1 className="text-3xl font-semibold mb-6 text-black text-center">
            Đổi mật khẩu
          </h1>
          <form onSubmit={handleSubmit} className="space-y-4">
            {/* Your form elements go here */}
            <div>
              <label
                htmlFor="username"
                className="block text-sm font-medium text-gray-700"
              >
                Mật khẩu cũ
              </label>
              <input
                type="password"
                id="oldPassword"
                placeholder="Nhập mật khẩu cũ..."
                name="oldPassword"
                className="mt-1 p-2 w-full border rounded-md focus:border-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-300 transition-colors duration-300"
                required
              />
            </div>
            <div>
              <label
                htmlFor="password"
                className="block text-sm font-medium text-gray-700"
              >
                Mật khẩu mới
              </label>
              <input
                type="password"
                id="password"
                placeholder="Nhập mật khẩu mới..."
                name="password"
                className="mt-1 p-2 w-full border rounded-md focus:border-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-300 transition-colors duration-300"
                required
              />
            </div>
            <div>
              <label
                htmlFor="confirmPassword"
                className="block text-sm font-medium text-gray-700"
              >
                Xác nhận mật khẩu
              </label>
              <input
                type="password"
                id="confirmPassword"
                placeholder="Nhập xác nhận mật khẩu..."
                name="confirmPassword"
                className="mt-1 p-2 w-full border rounded-md focus:border-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-300 transition-colors duration-300"
                required
              />
            </div>

            <div>
              {formState.errors.length > 0 && (
                <ul>
                  {formState.errors.map((error, index) => (
                    <li className="text-red-400" key={index}>
                      {error}
                    </li>
                  ))}
                </ul>
              )}
            </div>
            <div>
              <button
                type="submit"
                className="w-full bg-black text-white p-2 rounded-md hover:bg-gray-800 focus:outline-none focus:bg-black focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-900 transition-colors duration-300"
              >
                Đổi mật khẩu
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ChangePassword;
