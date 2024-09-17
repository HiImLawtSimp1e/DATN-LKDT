import CategoryList from "@/components/dashboard/category/category-list";
import AdminLoading from "@/components/dashboard/loading";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

interface IProps {
  page?: number;
  searchText?: string;
}

const Categories = async ({ page, searchText }: IProps) => {
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "";

  if (searchText == null || searchText == undefined) {
    url = "http://localhost:5000/api/Category/admin";
  } else {
    url = `http://localhost:5000/api/Category/admin/search/${searchText}`;
  }

  if (!(page == null || page <= 0)) {
    url += `?page=${page}`;
  }

  const res = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // header Authorization
    },
    next: { tags: ["categoryListAdmin"] },
  });

  const responseData: ApiResponse<PagingParams<ICategory[]>> = await res.json();
  const { data, success, message } = responseData;
  // console.log(responseData);
  const { result, pages, currentPage } = data;

  return (
    <CategoryList categories={result} pages={pages} currentPage={currentPage} />
  );
};

const CategoriesPage = ({
  searchParams,
}: {
  searchParams: { page?: number; searchText?: string };
}) => {
  // Destructure page và searchText từ searchParams
  const { page, searchText } = searchParams;

  // Render component Categories với các prop
  return (
    <Suspense fallback={<AdminLoading />}>
      <Categories
        page={page || undefined}
        searchText={searchText || undefined}
      />
    </Suspense>
  );
};

export default CategoriesPage;
