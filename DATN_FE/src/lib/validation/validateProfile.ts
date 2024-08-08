export const validateAddress = (
  name: string,
  email: string,
  phoneNumber: string,
  address: string
): [string[], boolean] => {
  const errors: string[] = [];

  if (!name || name.trim().length === 0) {
    errors.push("Bạn chưa nhập họ và tên");
  } else if (name.length < 6) {
    errors.push("Họ tên không được ngắn hơn 6 ký tự");
  } else if (name.length > 50) {
    errors.push("Họ tên không được dài hơn 50 ký tự");
  }

  if (!email || email.trim().length === 0) {
    errors.push("Bạn chưa nhập Email");
  } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
    errors.push("Email không hợp lệ");
  }

  if (!phoneNumber || phoneNumber.trim().length === 0) {
    errors.push("Bạn chưa nhập số điện thoại");
  } else if (!/^(\+?\d{1,3})?0?\d{9}$/.test(phoneNumber)) {
    errors.push("Số điện thoại không hợp lệ");
  }

  if (!address || address.trim().length === 0) {
    errors.push("Bạn chưa nhập địa chỉ");
  } else if (address.length < 6) {
    errors.push("Địa chỉ không được ngắn hơn 6 ký tự");
  } else if (address.length > 250) {
    errors.push("Địa chỉ không được dài hơn 250 ký tự");
  }

  return [errors, errors.length === 0];
};
