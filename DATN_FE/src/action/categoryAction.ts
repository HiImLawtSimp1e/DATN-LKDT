"use server";

import { revalidatePath, revalidateTag } from "next/cache";
import slugify from "slugify";

// Định nghĩa hàm addCategory
export const addCategory = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const title = formData.get("title") as string;
  const slug = slugify(title, { lower: true });

  try {
    const res = await fetch(`http://localhost:5000/api/Category/admin`, {
      method: "POST",
      body: JSON.stringify({ title, slug }),
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
      revalidateTag("categoryListAdmin");
      revalidateTag("categorySelect");
      revalidatePath("/product");
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

// Định nghĩa hàm updateCategory
export const updateCategory = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const id = formData.get("id") as string;
  const title = formData.get("title") as string;
  const isActive = formData.get("isActive") === "true";
  const slug = slugify(title, { lower: true });

  try {
    const res = await fetch(`http://localhost:5000/api/Category/admin/${id}`, {
      method: "PUT",
      body: JSON.stringify({ title, slug, isActive }),
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
      revalidateTag("categoryListAdmin");
      revalidateTag("categorySelect");
      revalidateTag("categoryDetail");
      revalidatePath("/product");
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

export const deleteCategory = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as string;

  const res = await fetch(`http://localhost:5000/api/Category/admin/${id}`, {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
  });

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { success, message } = responseData;

  if (success) {
    // Nếu thành công, làm mới lại đường dẫn và chuyển hướng
    revalidateTag("categoryListAdmin");
    revalidateTag("categorySelect");
    revalidatePath("/product");
    return { success: true, errors: [] };
  } else {
    return { errors: [message] };
  }
};
