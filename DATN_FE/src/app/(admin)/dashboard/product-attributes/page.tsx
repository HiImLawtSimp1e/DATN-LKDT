import ProductAttributeList from "@/components/dashboard/product-attribute/product-attribute-list";
import { cookies as nextCookies } from "next/headers";

interface IProps {
  page?: number;
  searchText?: string;
}

const ProductAttributes = async ({ page, searchText }: IProps) => {
  // Lấy access token từ cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "";

  if (searchText == null || searchText == undefined) {
    url = "http://localhost:5000/api/ProductAttribute";
  } else {
    url = `http://localhost:5000/api/ProductAttribute/admin/search/${searchText}`;
  }

  if (!(page == null || page <= 0)) {
    url += `?page=${page}`;
  }

  const res = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // header Authorization
    },
    next: { tags: ["productAttributeList"] },
  });

  const responseData: ApiResponse<PagingParams<IProductAttribute[]>> =
    await res.json();
  const { data, success, message } = responseData;
  const { result, pages, currentPage } = data;

  return (
    <ProductAttributeList
      productAttributes={result}
      pages={pages}
      currentPage={currentPage}
    />
  );
};

const ProductAttributesPage = ({
  searchParams,
}: {
  searchParams: { page?: number; searchText?: string };
}) => {
  // Destructure page và searchText từ searchParams
  const { page, searchText } = searchParams;

  // Render component Product Attributes với các prop
  return (
    <ProductAttributes
      page={page || undefined}
      searchText={searchText || undefined}
    />
  );
};

export default ProductAttributesPage;
