import AdminLoading from "@/components/dashboard/loading";
import PostList from "@/components/dashboard/post/post-list";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

interface IProps {
  page?: number;
  searchText?: string;
}

const Posts = async ({ page, searchText }: IProps) => {
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  let url = "";

  if (searchText == null || searchText == undefined) {
    url = "http://localhost:5000/api/Blog/admin";
  } else {
    url = `http://localhost:5000/api/Blog/admin/search/${searchText}`;
  }

  if (!(page == null || page <= 0)) {
    url += `?page=${page}`;
  }

  const res = await fetch(url, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // header Authorization
    },
    next: { tags: ["postListAdmin"] },
  });

  const responseData: ApiResponse<PagingParams<IPost[]>> = await res.json();
  const { data, success, message } = responseData;
  // console.log(responseData);
  const { result, pages, currentPage } = data;

  return <PostList posts={result} pages={pages} currentPage={currentPage} />;
};

const PostsPage = ({
  searchParams,
}: {
  searchParams: { page?: number; searchText?: string };
}) => {
  // Destructure page và searchText từ searchParams
  const { page, searchText } = searchParams;

  // Render Posts component with params prop
  return (
    <Suspense fallback={<AdminLoading />}>
      <Posts page={page || undefined} searchText={searchText || undefined} />
    </Suspense>
  );
};

export default PostsPage;
