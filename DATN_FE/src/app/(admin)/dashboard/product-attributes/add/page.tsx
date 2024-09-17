import AdminLoading from "@/components/dashboard/loading";
import AddProductAttributeForm from "@/components/dashboard/product-attribute/add-product-attribute-form";
import { Suspense } from "react";

const AddProductAttributePage = () => {
  return (
    <Suspense fallback={<AdminLoading />}>
      <AddProductAttributeForm />
    </Suspense>
  );
};

export default AddProductAttributePage;
