import AdminLoading from "@/components/dashboard/loading";
import ProductTypeList from "@/components/dashboard/productType/product-type-list";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

interface IProps {
  page?: number;
  searchText?: string;
}

const ProductTypes = async ({ page, searchText }: IProps) => {
  // Lấy access token từ cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "";

  if (searchText == null || searchText == undefined) {
    url = "http://localhost:5000/api/ProductType";
  } else {
    url = `http://localhost:5000/api/ProductType/admin/search/${searchText}`;
  }

  if (!(page == null || page <= 0)) {
    url += `?page=${page}`;
  }

  const res = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // header Authorization
    },
    next: { tags: ["productTypeList"] },
  });

  const responseData: ApiResponse<PagingParams<IProductType[]>> =
    await res.json();
  const { data, success, message } = responseData;
  // console.log(responseData);
  const { result, pages, currentPage } = data;

  return (
    <ProductTypeList
      productTypes={result}
      pages={pages}
      currentPage={currentPage}
    />
  );
};

const ProductTypesPage = ({
  searchParams,
}: {
  searchParams: { page?: number; searchText?: string };
}) => {
  // Destructure page và searchText từ searchParams
  const { page, searchText } = searchParams;

  // Render component Product Types với các prop
  return (
    <Suspense fallback={<AdminLoading />}>
      <ProductTypes
        page={page || undefined}
        searchText={searchText || undefined}
      />
    </Suspense>
  );
};

export default ProductTypesPage;
