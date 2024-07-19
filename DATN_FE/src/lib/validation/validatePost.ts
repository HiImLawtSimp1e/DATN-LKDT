export const validatePost = (
  title: string,
  content: string,
  description: string,
  seoTitle: string,
  seoDescription: string,
  seoKeyworks: string
): [string[], boolean] => {
  const errors: string[] = [];

  if (!title || title.trim().length === 0) {
    errors.push("Tiêu đề bài viết là bắt buộc.");
  } else if (title.trim().length < 2) {
    errors.push("Tiêu đề bài viết phải có ít nhất 2 ký tự.");
  }

  if (description && description.trim().length > 250) {
    errors.push("Mô tả bài viết không được dài quá 250 ký tự");
  }

  if (!content || content.trim().length === 0) {
    errors.push("Nội dung bài viết là bắt buộc.");
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
