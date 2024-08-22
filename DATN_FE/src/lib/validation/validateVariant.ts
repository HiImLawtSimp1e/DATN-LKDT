export const validateVariant = (
  productTypeId: string,
  price: number | null,
  originalPrice: number | null,
  quantity?: number | null
): [string[], boolean] => {
  const errors: string[] = [];
  const maxIntValue = 2147483647;

  // Validate product type ID
  if (!productTypeId) {
    errors.push("Loại sản phẩm là bắt buộc.");
  }

  // Validate price
  if (price === null || price === undefined) {
    errors.push("Giá bán là bắt buộc.");
  } else if (price < 1000) {
    errors.push("Giá bán phải là số nguyên và lớn hơn hoặc bằng 1000.");
  } else if (price > maxIntValue) {
    errors.push(`Giá bán không được lớn hơn ${maxIntValue}.`);
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
      errors.push(`Giá gốc không được lớn hơn ${maxIntValue}.`);
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

  // Validate quantity
  if (quantity != null && quantity != undefined && quantity < 0) {
    errors.push("Số lượng phải là số nguyên.");
  }

  // Return errors and a boolean indicating validity
  return [errors, errors.length === 0];
};
