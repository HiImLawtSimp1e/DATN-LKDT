export const validateVoucher = (
  voucherName: string,
  discountValue: number | null,
  minOrderCondition: number | null,
  maxDiscountValue: number | null,
  quantity: number | null,
  startDate: string,
  endDate: string,
  code: string,
  isDiscountPercent: boolean
): [string[], boolean] => {
  const errors: string[] = [];
  const maxIntValue = 2147483647;

  if (!code || code.trim().length === 0) {
    errors.push("Không được bỏ trống mã giảm giá");
  } else if (code.trim().length < 2 || code.trim().length > 25) {
    errors.push("Mã giảm giá phải từ 2 đến 25 ký tự");
  }

  if (!voucherName || voucherName.trim().length === 0) {
    errors.push("Tên voucher không được để trống");
  } else if (voucherName.trim().length < 2 || voucherName.trim().length > 50) {
    errors.push("Tên voucher phải từ 2 đến 25 ký tự");
  }

  if (discountValue === null) {
    errors.push("Không được để trống giá trị giảm giá");
  }

  if (isDiscountPercent === true) {
    if (discountValue !== null && (discountValue < 1 || discountValue > 80)) {
      errors.push("Giá trị giảm giá(theo phần trăm) phải từ khoảng 1% đến 80%");
    }
  } else {
    if (discountValue !== null && discountValue < 10000) {
      errors.push("Giả trị giảm giá(cố định) phải ít nhất là 10000đ");
    } else if (discountValue !== null && discountValue > maxIntValue) {
      errors.push(
        `Giả trị giảm giá(cố định) không được vượt quá ${maxIntValue}.`
      );
    }
  }

  if (minOrderCondition !== null && minOrderCondition < 0) {
    errors.push(
      "Tổng giá trị đơn hàng tối thiểu để sử dụng voucher không được âm"
    );
  } else if (minOrderCondition !== null && minOrderCondition > maxIntValue) {
    errors.push(
      `Tổng giá trị đơn hàng tối thiểu để sử dụng voucher không được vượt quá ${maxIntValue}.`
    );
  }

  if (maxDiscountValue !== null && maxDiscountValue < 0) {
    errors.push("Giá trị giảm giá tối đa không được âm");
  } else if (maxDiscountValue !== null && maxDiscountValue > maxIntValue) {
    errors.push(`Giá trị giảm giá tối đa không được vượt quá ${maxIntValue}.`);
  }

  if (quantity !== null && quantity < 0) {
    errors.push("Số lượng voucher không được âm");
  }

  const startDateObj = new Date(startDate);
  const endDateObj = new Date(endDate);

  if (isNaN(startDateObj.getTime())) {
    errors.push("Ngày bắt đầu áp dụng voucher không hợp lệ");
  }

  if (isNaN(endDateObj.getTime())) {
    errors.push("Ngày hết hạn voucher không hợp lệ");
  }

  if (startDateObj >= endDateObj) {
    errors.push("Ngày hết hạn voucher không được sớm hơn ngày bắt đầu áp dụng");
  }

  return [errors, errors.length === 0];
};
