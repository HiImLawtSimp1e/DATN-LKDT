"use server";

import { uploadImage } from "@/lib/cloudinary/cloudinary";
import { validatePost } from "@/lib/validation/validatePost";
import { revalidatePath, revalidateTag } from "next/cache";
import slugify from "slugify";

interface PostFormData {
  title: string;
  slug: string;
  description: string;
  image?: string;
  content: string;
  seoTitle: string;
  seoDescription: string;
  seoKeyworks: string;
  isActive?: boolean | null;
}

export const addPost = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const title = formData.get("title") as string;
  const description = formData.get("description") as string;
  const imageFile = formData.get("image") as File;
  const content = formData.get("content") as string;
  const seoTitle = formData.get("seoTitle") as string;
  const seoDescription = formData.get("seoDescription") as string;
  const seoKeyworks = formData.get("seoKeyworks") as string;
  const slug = slugify(title);

  let image = "";

  if (imageFile) {
    const result = await uploadImage(imageFile, ["post-image"]);
    if (result && result.secure_url) {
      image = result.secure_url;
    }
  } else {
    return { errors: ["No file found"] };
  }

  const [errors, isValid] = validatePost(
    title,
    content,
    description,
    seoTitle,
    seoDescription,
    seoKeyworks
  );

  if (!isValid) {
    return { errors };
  }

  const postData: PostFormData = {
    title,
    slug,
    content,
    description,
    image,
    seoTitle,
    seoDescription,
    seoKeyworks,
  };

  try {
    const res = await fetch("http://localhost:5000/api/Post/admin", {
      method: "POST",
      body: JSON.stringify(postData),
      headers: { "Content-Type": "application/json" },
    });

    if (!res.ok) {
      // Nếu không thành công, phân tích phản hồi lỗi
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo mảng để chứa thông báo lỗi
      let errorMessages: string[] = [];

      // Kiểm tra và thêm thông báo lỗi vào mảng
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }
      // Trả về trạng thái mới với các lỗi
      return { errors: errorMessages };
    }

    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu thành công, làm mới lại đường dẫn và chuyển hướng
      revalidatePath("/dashboard/posts");
      revalidateTag("shopPostList");
      return { success: true, errors: [] };
    } else {
      return { errors: [message] };
    }
  } catch (error) {
    // Xử lý các lỗi không mong muốn
    console.error("Lỗi không mong muốn:", error);
    return { errors: ["Đã xảy ra lỗi không mong muốn. Vui lòng thử lại."] };
  }
};

export const updatePost = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as string;
  const title = formData.get("title") as string;
  const image = formData.get("image") as string;
  const description = formData.get("description") as string;
  const content = formData.get("content") as string;
  const seoTitle = formData.get("seoTitle") as string;
  const seoDescription = formData.get("seoDescription") as string;
  const seoKeyworks = formData.get("seoKeyworks") as string;
  const isActive = formData.get("isActive") === "true";
  const slug = slugify(title);

  const [errors, isValid] = validatePost(
    title,
    content,
    description,
    seoTitle,
    seoDescription,
    seoKeyworks
  );

  if (!isValid) {
    return { errors };
  }

  const postData: PostFormData = {
    title,
    slug,
    content,
    description,
    image,
    seoTitle,
    seoDescription,
    seoKeyworks,
    isActive,
  };

  try {
    const res = await fetch(`http://localhost:5000/api/Post/admin/${id}`, {
      method: "PUT",
      body: JSON.stringify(postData),
      headers: { "Content-Type": "application/json" },
    });

    if (!res.ok) {
      // Nếu không thành công, phân tích phản hồi lỗi
      const errorResponse = await res.json();
      const { errors } = errorResponse;

      // Tạo mảng để chứa thông báo lỗi
      let errorMessages: string[] = [];

      // Kiểm tra và thêm thông báo lỗi vào mảng
      if (errors) {
        for (const key in errors) {
          if (errors.hasOwnProperty(key)) {
            errorMessages = errorMessages.concat(errors[key]);
          }
        }
      }
      // Trả về trạng thái mới với các lỗi
      return { errors: errorMessages };
    }

    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu thành công, làm mới lại đường dẫn và chuyển hướng
      revalidatePath("/dashboard/posts");
      revalidateTag("postDetail");
      revalidateTag("shopPostList");
      return { success: true, errors: [] };
    } else {
      return { errors: [message] };
    }
  } catch (error) {
    // Xử lý các lỗi không mong muốn
    console.error("Lỗi không mong muốn:", error);
    return { errors: ["Đã xảy ra lỗi không mong muốn. Vui lòng thử lại."] };
  }
};

export const deletePost = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as number | null;

  const res = await fetch(`http://localhost:5000/api/Post/admin/${id}`, {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
  });

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { success, message } = responseData;

  if (success) {
    // Nếu thành công, làm mới lại đường dẫn và chuyển hướng
    revalidatePath("/dashboard/posts");
    return { success: true, errors: [] };
  } else {
    return { errors: [message] };
  }
};
