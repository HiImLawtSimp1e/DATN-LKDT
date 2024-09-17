import AddCategoryForm from "@/components/dashboard/category/add-category-form";
import AdminLoading from "@/components/dashboard/loading";
import { Suspense } from "react";

const AddCategoryPage = () => {
  return (
    <Suspense fallback={<AdminLoading />}>
      <AddCategoryForm />;
    </Suspense>
  );
};

export default AddCategoryPage;
