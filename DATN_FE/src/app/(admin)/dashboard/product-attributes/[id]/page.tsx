import AdminLoading from "@/components/dashboard/loading";
import UpdateProductAttributeForm from "@/components/dashboard/product-attribute/update-product-attribute-form";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

const ProductType = async ({ id }: { id: string }) => {
  // Lấy access token từ cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(`http://localhost:5000/api/ProductAttribute/${id}`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // header Authorization
    },
    next: { tags: ["productAttributeDetail"] },
  });

  const productAttribute: ApiResponse<IProductAttribute> = await res.json();

  return <UpdateProductAttributeForm attribute={productAttribute.data} />;
};

const UpdateProductAttributePage = ({ params }: { params: { id: string } }) => {
  const { id } = params;
  return (
    <Suspense fallback={<AdminLoading />}>
      <ProductType id={id} />
    </Suspense>
  );
};

export default UpdateProductAttributePage;
