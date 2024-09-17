import Loading from "@/components/shop/loading";
import ChangePassword from "@/components/shop/profile/change-password";
import { Suspense } from "react";

const ChangePasswordPage = () => {
  return (
    <Suspense fallback={<Loading />}>
      <ChangePassword />
    </Suspense>
  );
};

export default ChangePasswordPage;
