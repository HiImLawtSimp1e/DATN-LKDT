import AdminLoading from "@/components/dashboard/loading";
import ProductList from "@/components/dashboard/product/product-list";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

interface IProps {
  page?: number;
  searchText?: string;
}

const Products = async ({ page, searchText }: IProps) => {
  // Lấy access token từ cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "";

  if (searchText == null || searchText == undefined) {
    url = "http://localhost:5000/api/Product/admin";
  } else {
    url = `http://localhost:5000/api/Product/admin/search/${searchText}`;
  }

  if (!(page == null || page <= 0)) {
    url += `?page=${page}`;
  }

  //console.log(url);

  const res = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // header Authorization
    },
    next: { tags: ["productListAdmin"] },
  });

  const responseData: ApiResponse<PagingParams<IProduct[]>> = await res.json();
  //console.log(responseData);
  const { data, success, message } = responseData;
  const { result, pages, currentPage } = data;

  return (
    <ProductList products={result} pages={pages} currentPage={currentPage} />
  );
};

const ProductsPage = ({
  searchParams,
}: {
  searchParams: { page?: number; searchText?: string };
}) => {
  // Destructure page và searchText từ searchParams
  const { page, searchText } = searchParams;

  // Render component Products với các prop
  return (
    <Suspense fallback={<AdminLoading />}>
      <Products page={page || undefined} searchText={searchText || undefined} />
    </Suspense>
  );
};

export default ProductsPage;
