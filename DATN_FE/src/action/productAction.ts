"use server";

import { uploadImage } from "@/lib/cloudinary/cloudinary";
import {
  validateAddProduct,
  validateUpdateProduct,
} from "@/lib/validation/validateProduct";
import { revalidatePath, revalidateTag } from "next/cache";
import slugify from "slugify";

// Định nghĩa lại interface ProductFormData
interface ProductFormData {
  title: string;
  description: string;
  imageUrl?: string;
  seoTitle: string;
  seoDescription: string;
  seoKeyworks: string;
  slug: string;
  isActive?: boolean | null;
  categoryId?: string;
  productTypeId?: string;
  price?: number | null;
  originalPrice?: number | null;
}

export const addProduct = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const title = formData.get("title") as string;
  const description = formData.get("description") as string;
  const image = formData.get("image") as File;
  const seoTitle = formData.get("seoTitle") as string;
  const seoDescription = formData.get("seoDescription") as string;
  const seoKeyworks = formData.get("seoKeyworks") as string;
  const categoryId = formData.get("categoryId") as string;
  const productTypeId = formData.get("productTypeId") as string;
  const price = formData.get("price") ? Number(formData.get("price")) : null;
  const originalPrice = formData.get("originalPrice")
    ? Number(formData.get("originalPrice"))
    : 0;
  const slug = slugify(title, { lower: true });

  let imageUrl = "";

  if (image) {
    const result = await uploadImage(image, ["product-image"]);
    if (result && result.secure_url) {
      imageUrl = result.secure_url;
    }
  } else {
    return { errors: ["Không tìm thấy tệp"] };
  }

  // Kiểm tra dữ liệu đầu vào từ người dùng
  const [errors, isValid] = validateAddProduct(
    title,
    description,
    seoTitle,
    seoDescription,
    seoKeyworks,
    price,
    originalPrice
  );

  if (!isValid) {
    return { errors };
  }

  const productData: ProductFormData = {
    title,
    description,
    imageUrl,
    seoTitle,
    seoDescription,
    seoKeyworks,
    slug,
    categoryId,
    productTypeId,
    price,
    originalPrice,
  };

  try {
    const res = await fetch("http://localhost:5000/api/Product/admin", {
      method: "POST",
      body: JSON.stringify(productData),
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
      revalidatePath("/dashboard/products");
      revalidateTag("shopProductDetail");
      revalidateTag("shopProductList");
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

export const updateProduct = async (
  prevState: FormState,
  formData: FormData
) => {
  const id = formData.get("id") as string;
  const title = formData.get("title") as string;
  const description = formData.get("description") as string;
  const imageUrl = formData.get("imageUrl") as string;
  const seoTitle = formData.get("seoTitle") as string;
  const seoDescription = formData.get("seoDescription") as string;
  const seoKeyworks = formData.get("seoKeyworks") as string;
  const isActive = formData.get("isActive") === "true";
  const categoryId = formData.get("categoryId") as string;
  const slug = slugify(title, { lower: true });

  // Kiểm tra dữ liệu đầu vào từ người dùng
  const [errors, isValid] = validateUpdateProduct(
    title,
    description,
    seoTitle,
    seoDescription,
    seoKeyworks
  );

  if (!isValid) {
    return { errors };
  }

  const productData: ProductFormData = {
    title,
    description,
    imageUrl,
    seoTitle,
    seoDescription,
    seoKeyworks,
    slug,
    categoryId,
    isActive,
  };

  try {
    const res = await fetch(`http://localhost:5000/api/Product/admin/${id}`, {
      method: "PUT",
      body: JSON.stringify(productData),
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

    // Nếu thành công, phân tích dữ liệu phản hồi
    const responseData: ApiResponse<string> = await res.json();
    // console.log(responseData);
    const { success, message } = responseData;

    if (success) {
      // Nếu thành công, làm mới lại đường dẫn và chuyển hướng
      revalidatePath("/dashboard/products");
      revalidateTag("productDetailAdmin");
      revalidateTag("shopProductDetail");
      revalidateTag("shopProductList");
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

export const deleteProduct = async (
  prevState: FormState,
  formData: FormData
): Promise<FormState | undefined> => {
  const id = formData.get("id") as number | null;

  const res = await fetch(`http://localhost:5000/api/Product/admin/${id}`, {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
  });

  const responseData: ApiResponse<string> = await res.json();
  // console.log(responseData);
  const { success, message } = responseData;

  if (success) {
    // Nếu thành công, làm mới lại đường dẫn và chuyển hướng
    revalidatePath("/dashboard/products");
    revalidateTag("shopProductList");
    revalidatePath("/");
    revalidatePath("/product");
    return { success: true, errors: [] };
  } else {
    return { errors: [message] };
  }
};
