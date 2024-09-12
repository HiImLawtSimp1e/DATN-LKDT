export const validatePost = (
  title: string,
  content: string,
  description: string,
  seoTitle: string,
  seoDescription: string,
  seoKeyworks: string
): [string[], boolean] => {
  const errors: string[] = [];

  // Kiểm tra tiêu đề
  if (!title || title.trim().length === 0) {
    errors.push("Tiêu đề bài viết là bắt buộc.");
  } else if (title.trim().length > 250) {
    errors.push("Tiêu đề không được dài hơn 250 ký tự.");
  }

  // Kiểm tra mô tả
  if (description && description.trim().length > 250) {
    errors.push("Mô tả không được dài hơn 250 ký tự.");
  }

  // Kiểm tra nội dung
  if (!content || content.trim().length === 0) {
    errors.push("Nội dung bài viết là bắt buộc.");
  }

  // Kiểm tra tiêu đề SEO
  if (seoTitle && seoTitle.trim().length > 250) {
    errors.push("Tiêu đề SEO không được dài hơn 250 ký tự.");
  }

  // Kiểm tra mô tả SEO
  if (seoDescription && seoDescription.trim().length > 160) {
    errors.push("Mô tả SEO không được dài hơn 160 ký tự.");
  }

  // Kiểm tra từ khóa SEO
  if (seoKeyworks && seoKeyworks.trim().length > 100) {
    errors.push("Từ khóa SEO không được dài hơn 100 ký tự.");
  }

  return [errors, errors.length === 0];
};
