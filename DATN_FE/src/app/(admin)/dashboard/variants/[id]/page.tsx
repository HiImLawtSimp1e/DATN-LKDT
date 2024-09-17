import AdminLoading from "@/components/dashboard/loading";
import UpdateVariantForm from "@/components/dashboard/variant/update-variant-form";
import { Suspense } from "react";

interface IProps {
  productId: string;
  productTypeId: string;
}

const Variant = async ({ productId, productTypeId }: IProps) => {
  const res = await fetch(
    `http://localhost:5000/api/ProductVariant/${productId}/?productTypeId=${productTypeId}`,
    {
      method: "GET",
      next: { tags: ["getVariant"] },
    }
  );

  const variant: ApiResponse<IProductVariant> = await res.json();

  // console.log(variant.data);

  return <UpdateVariantForm variant={variant.data} />;
};

const UpdateVariantPage = ({
  searchParams,
  params,
}: {
  searchParams: { productId: string };
  params: { id: string };
}) => {
  const { productId } = searchParams;
  const { id } = params;
  return (
    <Suspense fallback={<AdminLoading />}>
      <Variant productId={productId} productTypeId={id} />
    </Suspense>
  );
};

export default UpdateVariantPage;
