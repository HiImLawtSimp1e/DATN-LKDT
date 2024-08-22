export const validateAddProduct = (
  title: string,
  description: string,
  seoTitle: string,
  seoDescription: string,
  seoKeyworks: string,
  price: number | null,
  originalPrice: number | null
): [string[], boolean] => {
  const errors: string[] = [];
  const maxIntValue = 2147483647;

  if (!title || title.trim().length === 0) {
    errors.push("Tiêu đề sản phẩm là bắt buộc.");
  } else if (title.trim().length < 2) {
    errors.push("Tiêu đề sản phẩm phải có ít nhất 2 ký tự.");
  }

  if (!description || description.trim().length === 0) {
    errors.push("Mô tả sản phẩm là bắt buộc.");
  } else if (description.trim().length < 6) {
    errors.push("Mô tả sản phẩm phải có ít nhất 6 ký tự.");
  }

  if (seoTitle && seoTitle.trim().length > 70) {
    errors.push("Tiêu đề SEO không được dài hơn 70 ký tự.");
  }

  if (seoDescription && seoDescription.trim().length > 160) {
    errors.push("Mô tả SEO không được dài hơn 160 ký tự.");
  }

  if (seoKeyworks && seoKeyworks.trim().length > 100) {
    errors.push("Từ khóa SEO không được dài hơn 100 ký tự.");
  }

  // Validate price
  if (price === null || price === undefined) {
    errors.push("Giá bán là bắt buộc.");
  } else if (price < 1000) {
    errors.push("Giá bán phải là số nguyên và lớn hơn hoặc bằng 1000.");
  } else if (price > maxIntValue) {
    errors.push(`Giá bán không được vượt quá ${maxIntValue}.`);
  }

  // Validate original price
  if (
    originalPrice !== undefined &&
    originalPrice != null &&
    originalPrice < 0
  ) {
    errors.push("Giá gốc phải là số nguyên dương");
  }
  if (
    originalPrice !== null &&
    originalPrice !== undefined &&
    originalPrice !== 0
  ) {
    if (originalPrice < 1000) {
      errors.push("Giá gốc phải lớn hơn hoặc bằng 1000");
    } else if (originalPrice > maxIntValue) {
      errors.push(`Giá gốc không được vượt quá ${maxIntValue}.`);
    }
  }

  // Validate that original price is greater than or equal to price
  if (
    originalPrice !== null &&
    originalPrice !== undefined &&
    price !== null &&
    price !== undefined
  ) {
    if (originalPrice !== 0 && originalPrice < price) {
      errors.push("Giá gốc phải lớn hơn hoặc bằng giá bán");
    }
  }

  return [errors, errors.length === 0];
};

export const validateUpdateProduct = (
  title: string,
  description: string,
  seoTitle: string,
  seoDescription: string,
  seoKeyworks: string,
  price?: number | null,
  originalPrice?: number | null
): [string[], boolean] => {
  const errors: string[] = [];

  if (!title || title.trim().length === 0) {
    errors.push("Tiêu đề sản phẩm là bắt buộc.");
  } else if (title.trim().length < 2) {
    errors.push("Tiêu đề sản phẩm phải có ít nhất 2 ký tự.");
  }

  if (!description || description.trim().length === 0) {
    errors.push("Mô tả sản phẩm là bắt buộc.");
  } else if (description.trim().length < 6) {
    errors.push("Mô tả sản phẩm phải có ít nhất 6 ký tự.");
  }

  if (seoTitle && seoTitle.trim().length > 70) {
    errors.push("Tiêu đề SEO không được dài hơn 70 ký tự.");
  }

  if (seoDescription && seoDescription.trim().length > 160) {
    errors.push("Mô tả SEO không được dài hơn 160 ký tự.");
  }

  if (seoKeyworks && seoKeyworks.trim().length > 100) {
    errors.push("Từ khóa SEO không được dài hơn 100 ký tự.");
  }

  return [errors, errors.length === 0];
};
