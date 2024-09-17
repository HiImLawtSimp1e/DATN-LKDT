import AdminLoading from "@/components/dashboard/loading";
import AddProductTypeForm from "@/components/dashboard/productType/add-product-type-form";
import { Suspense } from "react";

const AddProductTypePage = () => {
  return (
    <Suspense fallback={<AdminLoading />}>
      <AddProductTypeForm />
    </Suspense>
  );
};

export default AddProductTypePage;
