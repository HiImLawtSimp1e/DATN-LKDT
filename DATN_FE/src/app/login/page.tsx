import CustomerLoginForm from "@/components/auth/customer-login-form";
import Loading from "@/components/shop/loading";
import { Metadata } from "next";
import { Suspense } from "react";

export const metadata: Metadata = {
  title: "FStore - Đăng nhập",
  description: "FStore - Đăng nhập",
};

const LoginPage = ({
  searchParams,
}: {
  searchParams: { redirectUrl?: string };
}) => {
  const { redirectUrl } = searchParams;
  return (
    <Suspense fallback={<Loading />}>
      <CustomerLoginForm redirectUrl={redirectUrl || null} />
    </Suspense>
  );
};
export default LoginPage;
