import AdminLoading from "@/components/dashboard/loading";
import UpdatePostForm from "@/components/dashboard/post/update-post-form";
import { cookies as nextCookies } from "next/headers";
import { Suspense } from "react";

const Post = async ({ id }: { id: string }) => {
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(`http://localhost:5000/api/Blog/admin/${id}`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`, // header Authorization
    },
    next: { tags: ["postDetail"] },
  });

  const post: ApiResponse<IPost> = await res.json();

  return <UpdatePostForm post={post.data} />;
};

const PostDetailPage = ({ params }: { params: { id: string } }) => {
  const { id } = params;
  return (
    <Suspense fallback={<AdminLoading />}>
      <Post id={id} />
    </Suspense>
  );
};

export default PostDetailPage;
