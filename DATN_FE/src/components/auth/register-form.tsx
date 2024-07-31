"use client";

import { registerAction } from "@/action/accountAction";
import { useCustomActionState } from "@/lib/custom/customHook";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import Link from "next/link";
import Image from "next/image";
import { setAuthPublic } from "@/service/auth-service/auth-service";

const RegisterForm = () => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    registerAction,
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
      toast.error("Register failed");
      setToastDisplayed(true); // Set toastDisplayed to true to prevent multiple toasts
    }
    if (formState.success) {
      setAuthPublic(formState.data?.toString() || "");
      router.push("/");
    }
  }, [formState, toastDisplayed]);

  return (
    <div className="flex min-h-[100vh]">
      {/* Left Pane */}
      <div className="w-full bg-gray-100 lg:w-1/2 flex items-center justify-center">
        <div className="max-w-md w-full p-6">
          <h1 className="text-3xl font-semibold mb-6 text-black text-center">
            Đăng ký
          </h1>
          <h1 className="text-sm font-semibold mb-6 text-gray-500 text-center">
            Tham gia với chúng tôi với quyền truy cập mọi lúc và miễn phí
          </h1>

          <form onSubmit={handleSubmit} className="space-y-4">
            {/* Your form elements go here */}
            <div>
              <label
                htmlFor="username"
                className="block text-sm font-medium text-gray-700"
              >
                Tài khoản
              </label>
              <input
                type="text"
                id="username"
                placeholder="Nhập tên tài khoản..."
                name="username"
                className="mt-1 p-2 w-full border rounded-md focus:border-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-300 transition-colors duration-300"
                required
              />
            </div>
            <div>
              <label
                htmlFor="password"
                className="block text-sm font-medium text-gray-700"
              >
                Mật khẩu
              </label>
              <input
                type="password"
                id="password"
                placeholder="*****"
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
                placeholder="*****"
                name="confirmPassword"
                className="mt-1 p-2 w-full border rounded-md focus:border-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-300 transition-colors duration-300"
                required
              />
            </div>
            <div>
              <label
                htmlFor="fullName"
                className="block text-sm font-medium text-gray-700"
              >
                Họ và tên
              </label>
              <input
                id="fullName"
                placeholder="Nhập họ và tên..."
                name="name"
                className="mt-1 p-2 w-full border rounded-md focus:border-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-300 transition-colors duration-300"
                required
              />
            </div>
            <div>
              <label
                htmlFor="email"
                className="block text-sm font-medium text-gray-700"
              >
                Email
              </label>
              <input
                type="email"
                id="email"
                placeholder="Nhập email của bạn..."
                name="email"
                className="mt-1 p-2 w-full border rounded-md focus:border-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-300 transition-colors duration-300"
                required
              />
            </div>
            <div>
              <label
                htmlFor="phoneNumber"
                className="block text-sm font-medium text-gray-700"
              >
                Số điện thoại
              </label>
              <input
                id="phoneNumber"
                placeholder="Nhập số điện thoại của bạn..."
                name="phoneNumber"
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
                Đăng ký
              </button>
            </div>
          </form>
          <div className="mt-4 text-md text-gray-600 text-center">
            <p>
              Bạn đã có tài khoản?{" "}
              <Link
                href="/login"
                className="font-bold text-blue-800 hover:opacity-85"
              >
                Đăng nhập
              </Link>
            </p>
          </div>
        </div>
      </div>
      {/* Right Pane */}
      <div className="relative hidden lg:flex items-center justify-center flex-1">
        <Image
          src="/register-banner.jpg"
          alt=""
          fill
          className="w-full h-full object-cover"
        />
      </div>
    </div>
  );
};
export default RegisterForm;
