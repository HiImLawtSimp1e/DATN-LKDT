import AdminLoading from "@/components/dashboard/loading";
import CreatePostForm from "@/components/dashboard/post/create-post-form";
import { Suspense } from "react";

const AddPostPage = () => {
  return (
    <Suspense fallback={<AdminLoading />}>
      <CreatePostForm />
    </Suspense>
  );
};

export default AddPostPage;
