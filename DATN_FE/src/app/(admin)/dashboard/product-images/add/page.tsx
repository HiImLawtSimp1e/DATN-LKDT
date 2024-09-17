import AdminLoading from "@/components/dashboard/loading";
import AddProductImageForm from "@/components/dashboard/product-image/add-image-form";
import { Suspense } from "react";

const AddProductImagePage = ({
  searchParams,
}: {
  searchParams: { productId: string };
}) => {
  const { productId } = searchParams;
  return (
    <Suspense fallback={<AdminLoading />}>
      <AddProductImageForm productId={productId} />
    </Suspense>
  );
};
export default AddProductImagePage;
