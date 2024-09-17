import AdminLoading from "@/components/dashboard/loading";
import AddVoucherForm from "@/components/dashboard/voucher/add-voucher-form";
import { Suspense } from "react";

const VoucherAddPage = () => {
  return (
    <Suspense fallback={<AdminLoading />}>
      <AddVoucherForm />
    </Suspense>
  );
};

export default VoucherAddPage;
