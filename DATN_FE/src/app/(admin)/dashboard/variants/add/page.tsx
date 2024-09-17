import AdminLoading from "@/components/dashboard/loading";
import AddVariantForm from "@/components/dashboard/variant/add-variant-form";
import { Suspense } from "react";

interface IProps {
  productId: string;
}

const Variant = async ({ productId }: IProps) => {
  const typeSelectRes = await fetch(
    `http://localhost:5000/api/ProductType/select/${productId}`,
    {
      method: "GET",
      next: { tags: ["selectProductType"] },
    }
  );

  const typeSelect: ApiResponse<IProductType[]> = await typeSelectRes.json();

  return <AddVariantForm productId={productId} typeSelect={typeSelect.data} />;
};

const AddVariantPage = ({
  searchParams,
}: {
  searchParams: { productId: string };
}) => {
  const { productId } = searchParams;
  return (
    <Suspense fallback={<AdminLoading />}>
      <Variant productId={productId} />
    </Suspense>
  );
};

export default AddVariantPage;
