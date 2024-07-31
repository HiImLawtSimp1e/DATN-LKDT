"use client";

import { customerLoginAction } from "@/action/accountAction";
import { useCustomActionState } from "@/lib/custom/customHook";
import { decodeSearchParam } from "@/lib/decode/decode";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import Link from "next/link";
import Image from "next/image";
import { setAuthPublic } from "@/service/auth-service/auth-service";
import GoogleSvg from "../ui/svg/googleSvg";
import FacebookSvg from "../ui/svg/facebookSvg";

interface IProps {
  redirectUrl: string | null;
}

const CustomerLoginForm = ({ redirectUrl }: IProps) => {
  const router = useRouter();

  const initialState: FormState = { errors: [] };
  const [formState, formAction] = useCustomActionState<FormState>(
    customerLoginAction,
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
      toast.error("Login failed");
      setToastDisplayed(true); // Set toastDisplayed to true to prevent multiple toasts
    }
    if (formState.success) {
      setAuthPublic(formState.data?.toString() || "");
      const url = redirectUrl !== null ? decodeSearchParam(redirectUrl) : "/";
      router.push(url);
    }
  }, [formState, toastDisplayed]);

  return (
    <div className="flex min-h-[100vh]">
      {/* Left Pane */}
      <div className="relative hidden lg:flex items-center justify-center flex-1">
        <Image
          src="/login-banner.jpg"
          alt=""
          fill
          className="w-full h-full object-cover"
        />
      </div>
      {/* Right Pane */}
      <div className="w-full bg-gray-100 lg:w-1/2 flex items-center justify-center">
        <div className="max-w-md w-full p-6">
          <h1 className="text-3xl font-semibold mb-6 text-black text-center">
            Chào mừng bạn đã quay trở lại
          </h1>
          <h1 className="text-sm font-semibold mb-6 text-gray-500 text-center">
            Hãy đăng nhập để tiếp tục
          </h1>
          <div className="mt-4 flex flex-col lg:flex-row items-center justify-between">
            <div className="w-full lg:w-1/2 mb-2 lg:mb-0">
              <button
                type="button"
                className="w-full flex justify-center items-center gap-2 bg-white text-sm text-gray-600 p-2 rounded-md hover:bg-gray-50 border border-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-200 transition-colors duration-300"
              >
                <GoogleSvg />
                Đăng nhập với Google
              </button>
            </div>
            <div className="w-full lg:w-1/2 ml-0 lg:ml-2">
              <button
                type="button"
                className="w-full flex justify-center items-center gap-2 bg-white text-sm text-gray-600 p-2 rounded-md hover:bg-gray-50 border border-gray-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-200 transition-colors duration-300"
              >
                <FacebookSvg />
                Đăng nhập với Facebook
              </button>
            </div>
          </div>
          <div className="mt-4 text-sm text-gray-600 text-center">
            <p>hoặc với tài khoản của bạn</p>
          </div>
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
                placeholder="Nhập tài khoản của bạn..."
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
                placeholder="*****"
                name="password"
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
                Đăng nhập
              </button>
            </div>
          </form>
          <div className="mt-4 text-md text-gray-600 text-center">
            <p>
              Bạn chưa có tài khoản?{" "}
              <Link
                href="/register"
                className="font-bold text-blue-800 hover:opacity-85"
              >
                Đăng ký
              </Link>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};
export default CustomerLoginForm;
