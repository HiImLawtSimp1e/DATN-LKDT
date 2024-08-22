"use server";

import { uploadImage } from "@/lib/cloudinary/cloudinary";
import { revalidatePath, revalidateTag } from "next/cache";
import { cookies as nextCookies } from "next/headers";

interface ProductImageFormData {
  imageUrl?: string;
  isActive?: boolean | null;
  isMain?: boolean | null;
  productId?: string;
}

export const AddProductImage = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  // Trích xuất các trường cần thiết từ formData
  const productId = formData.get("productId") as string;
  const image = formData.get("image") as File;
  const isMain = formData.get("isMain") === "true";

  let imageUrl = "";

  if (image) {
    const result = await uploadImage(image, ["product-image"]);
    if (result && result.secure_url) {
      imageUrl = result.secure_url;
    }
  } else {
    return { errors: ["Không tìm thấy tệp"] };
  }

  const productImageData: ProductImageFormData = {
    imageUrl,
    isMain,
    productId,
  };

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  try {
    const res = await fetch("http://localhost:5000/api/ProductImage/admin", {
      method: "POST",
      body: JSON.stringify(productImageData),
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });

    if (!res.ok) {
      // Xử lý lỗi từ server
      const errorResponse = await res.json();
      console.error(`Lỗi server: ${JSON.stringify(errorResponse)}`);

      // Kiểm tra xem phản hồi lỗi có chứa trường message không
      if (errorResponse && errorResponse.message) {
        return { errors: [errorResponse.message] };
      } else {
        return { errors: ["Đã xảy ra lỗi từ server."] };
      }
    }

    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
      revalidatePath(`/dashboard/products/${productId}`);
      revalidateTag("shopProductDetail");
      revalidatePath("/");
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

export const updateProductImage = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as string;
  const productId = formData.get("productId") as string;
  const imageUrl = formData.get("imageUrl") as string;
  const isMain = formData.get("isMain") === "true";
  const isActive = formData.get("isActive") === "true";

  const productImageData: ProductImageFormData = {
    imageUrl,
    isActive,
    isMain,
    productId,
  };

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  try {
    const res = await fetch(
      `http://localhost:5000/api/ProductImage/admin/${id}`,
      {
        method: "PUT",
        body: JSON.stringify(productImageData),
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (!res.ok) {
      // Xử lý lỗi từ server
      const errorResponse = await res.json();
      console.error(`Lỗi server: ${JSON.stringify(errorResponse)}`);

      // Kiểm tra xem phản hồi lỗi có chứa trường message không
      if (errorResponse && errorResponse.message) {
        return { errors: [errorResponse.message] };
      } else {
        return { errors: ["Đã xảy ra lỗi từ server."] };
      }
    }

    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
      revalidateTag("productDetailAdmin");
      revalidateTag("getProductImage");
      revalidateTag("shopProductDetail");
      revalidatePath("/");
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

export const deleteProductImage = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as string;

  //get access token form cookie
  const cookieStore = nextCookies();
  const token = cookieStore.get("authToken")?.value || "";

  const res = await fetch(
    `http://localhost:5000/api/ProductImage/admin/${id}`,
    {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    }
  );

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { success, message } = responseData;

  if (success) {
    // Nếu phản hồi thành công, cập nhật lại đường dẫn và chuyển hướng
    revalidateTag("productDetailAdmin");
    revalidateTag("shopProductDetail");
    revalidatePath("/");
    revalidatePath("/product");
    return { success: true, errors: [] };
  } else {
    return { errors: [message] };
  }
};
