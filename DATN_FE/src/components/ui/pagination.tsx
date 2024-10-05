import Link from "next/link";

interface IProps {
  pages: number;
  currentPage: number;
  pageSize?: number;
  clsColor: string;
}

const Pagination = ({ pages, currentPage, clsColor }: IProps) => {
  const renderPageNumbers = () => {
    const pageNumbers = [];

    if (pages <= 8) {
      for (let i = 1; i <= pages; i++) {
        pageNumbers.push(i);
      }
    } else {
      pageNumbers.push(1); // Trang đầu tiên

      // Hiển thị trang ở giữa
      if (currentPage > 4) {
        pageNumbers.push("...");
      }

      // Hiển thị các trang gần trang hiện tại
      for (
        let i = Math.max(2, currentPage - 1);
        i <= Math.min(pages - 1, currentPage + 1);
        i++
      ) {
        pageNumbers.push(i);
      }

      // Hiển thị dấu ba chấm trước trang cuối nếu cần
      if (currentPage < pages - 3) {
        pageNumbers.push("...");
      }

      pageNumbers.push(pages); // Trang cuối cùng
    }

    return pageNumbers;
  };

  return (
    <ul className="mt-5 float-right inline-flex -space-x-px text-sm">
      <li>
        <button
          className={`flex items-center justify-center px-3 h-8 ms-0 leading-tight border border-e-0 rounded-s-lg bg-${clsColor}-800 border-${clsColor}-700 text-${clsColor}-400 hover:bg-${clsColor}-700 ${
            currentPage === 1
              ? "cursor-not-allowed pointer-events-none opacity-50"
              : ""
          }`}
        >
          <Link href={currentPage === 1 ? "" : `?page=${currentPage - 1}`}>
            Trước
          </Link>
        </button>
      </li>

      {renderPageNumbers().map((page, index) =>
        typeof page === "number" ? (
          <li key={index}>
            <Link href={currentPage === page ? "" : `?page=${page}`}>
              <button
                className={`flex items-center justify-center px-3 h-8 leading-tight ${
                  currentPage === page
                    ? `border border-${clsColor}-700 bg-${clsColor}-700 text-white`
                    : `bg-${clsColor}-800 border-${clsColor}-700 text-${clsColor}-400 hover:bg-${clsColor}-700`
                }`}
              >
                {page}
              </button>
            </Link>
          </li>
        ) : (
          <li key={index}>
            <span className="flex items-center justify-center px-3 h-8 leading-tight text-gray-500">
              ...
            </span>
          </li>
        )
      )}

      <li>
        <button
          className={`flex items-center justify-center px-3 h-8 ms-0 leading-tight border border-e-0 rounded-e-lg bg-${clsColor}-800 border-${clsColor}-700 text-${clsColor}-400 hover:bg-${clsColor}-700 ${
            currentPage === pages
              ? "cursor-not-allowed pointer-events-none opacity-50"
              : ""
          }`}
        >
          <Link href={currentPage === pages ? "" : `?page=${currentPage + 1}`}>
            Sau
          </Link>
        </button>
      </li>
    </ul>
  );
};

export default Pagination;
