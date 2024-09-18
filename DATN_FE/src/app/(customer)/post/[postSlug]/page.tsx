import Loading from "@/components/shop/loading";
import ShopPostDetail from "@/components/shop/post-detail/post-detail";
import { Metadata } from "next";
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

export async function generateMetadata({
  params,
}: {
  params: { postSlug: string };
}): Promise<Metadata> {
  const { postSlug } = params; // Lấy postSlug từ params
  const res = await fetch(`http://localhost:5000/api/Blog/${postSlug}`, {
    method: "GET",
    next: { tags: ["shopPostDetail"] },
  });

  const post: ApiResponse<IPost> = await res.json();

  return {
    title: post?.data?.seoTitle || "FStore - Linh kiện điện tử chính hãng",
    description:
      post?.data?.seoDescription || "FStore - Linh kiện điện tử chính hãng",
    keywords:
      post?.data?.seoKeyworks || "FStore - Linh kiện điện tử chính hãng",
  };
}

const PostDetailPage = ({ params }: { params: { postSlug: string } }) => {
  const { postSlug } = params;
  return (
    <Suspense fallback={<Loading />}>
      <Post postSlug={postSlug} />
    </Suspense>
  );
};

export default PostDetailPage;
