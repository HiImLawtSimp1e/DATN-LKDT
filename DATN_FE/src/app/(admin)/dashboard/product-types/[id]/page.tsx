import UpdateProductTypeForm from "@/components/dashboard/productType/update-product-type-form";
import { cookies as nextCookies } from "next/headers";

const ProductType = async ({ id }: { id: string }) => {
  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(`http://localhost:5000/api/ProductType/${id}`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // header Authorization
    },
    next: { tags: ["productTypeDetail"] },
  });

  const productType: ApiResponse<IProductType> = await res.json();

  return <UpdateProductTypeForm type={productType.data} />;
};

const UpdateProductTypePage = ({ params }: { params: { id: string } }) => {
  const { id } = params;
  return <ProductType id={id} />;
};

export default UpdateProductTypePage;
