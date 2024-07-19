export const validateAddProduct = (
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

  // Validate price
  if (price === null || price === undefined) {
    errors.push("Giá sản phẩm là bắt buộc.");
  } else if (price < 1000) {
    errors.push("Giá sản phẩm phải là số nguyên và lớn hơn hoặc bằng 1000.");
  }

  // Validate original price
  if (originalPrice === null || originalPrice === undefined) {
    errors.push("Giá gốc là bắt buộc.");
  } else if (originalPrice < 1000) {
    errors.push("Giá gốc phải là số nguyên và lớn hơn hoặc bằng 1000.");
  }

  // Validate that original price is greater than or equal to price
  if (
    originalPrice !== null &&
    price !== null &&
    originalPrice !== undefined &&
    price !== undefined &&
    originalPrice < price
  ) {
    errors.push("Giá gốc phải lớn hơn hoặc bằng giá sản phẩm.");
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
