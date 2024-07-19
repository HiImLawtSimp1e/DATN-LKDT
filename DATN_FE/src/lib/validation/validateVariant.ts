export const validateVariant = (
  productTypeId: string,
  price: number | null,
  originalPrice: number | null,
  quantity?: number | null
): [string[], boolean] => {
  const errors: string[] = [];

  // Validate product type ID
  if (!productTypeId) {
    errors.push("Loại sản phẩm là bắt buộc.");
  }

  // Validate price
  if (price === null) {
    errors.push("Giá sản phẩm là bắt buộc.");
  } else if (price < 1000) {
    errors.push("Giá sản phẩm phải là số nguyên và lớn hơn hoặc bằng 1000.");
  }

  // Validate original price
  if (originalPrice === null) {
    errors.push("Giá gốc là bắt buộc.");
  } else if (originalPrice < 1000) {
    errors.push("Giá gốc phải là số nguyên và lớn hơn hoặc bằng 1000.");
  }

  // Validate that original price is greater than or equal to price
  if (originalPrice !== null && price !== null && originalPrice < price) {
    errors.push("Giá gốc phải lớn hơn hoặc bằng giá sản phẩm.");
  }

  // Validate quantity
  if (quantity != null && quantity != undefined && quantity < 0) {
    errors.push("Số lượng phải là số nguyên.");
  }

  // Return errors and a boolean indicating validity
  return [errors, errors.length === 0];
};
