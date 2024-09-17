import Loading from "@/components/shop/loading";
import ShopPostDetail from "@/components/shop/post-detail/post-detail";
import { Suspense } from "react";

const Post = async ({ postSlug }: { postSlug: string }) => {
  const res = await fetch(`http://localhost:5000/api/Blog/${postSlug}`, {
    method: "GET",
    next: { tags: ["shopPostDetail"] },
  });

  const post: ApiResponse<IPost> = await res.json();
  // console.log(post.data);

  return <ShopPostDetail post={post.data} />;
};

const PostDetailPage = ({ params }: { params: { postSlug: string } }) => {
  const { postSlug } = params;
  return (
    <Suspense fallback={<Loading />}>
      <Post postSlug={postSlug} />
    </Suspense>
  );
};

export default PostDetailPage;
