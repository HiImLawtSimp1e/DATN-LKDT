import AdminLoginForm from "@/components/auth/admin-login-form";
import Loading from "@/components/shop/loading";
import { Metadata } from "next";
import { Suspense } from "react";

export const metadata: Metadata = {
  title: "FStore - Đăng nhập",
  description: "FStore - Đăng nhập",
};

const LoginPage = () => {
  return (
    <div className="min-h-screen bg-gray-800 flex items-center justify-center">
      <Suspense fallback={<Loading />}>
        <AdminLoginForm />
      </Suspense>
    </div>
  );
};
export default LoginPage;
