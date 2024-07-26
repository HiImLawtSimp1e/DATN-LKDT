import ProductBanner from "@/components/shop/banner/productBanner";
import CategorySidebar from "@/components/shop/category-list/category-sidebar";
import Loading from "@/components/shop/loading";
import ShopProductList from "@/components/shop/product-list/product-list";
import { Suspense } from "react";

interface IProps {
  search: string | null;
  page?: number;
}

const Products = async ({ search, page }: IProps) => {
  let url = "";
  if (search != null) {
    if (page == null) {
      url = `http://localhost:5000/api/Product/search/${search}`;
    } else {
      url = `http://localhost:5000/api/Product/search/${search}?page=${page}`;
    }
  } else {
    if (page == null) {
      url = `http://localhost:5000/api/Product`;
    } else {
      url = `http://localhost:5000/api/Product?page=${page}`;
    }
  }
  const res = await fetch(url, {
    method: "GET",
    next: { tags: ["shopProductList"] },
  });

  const responseData: ApiResponse<PagingParams<IProduct[]>> = await res.json();
  const { data, success, message } = responseData;
  // console.log(responseData);
  const { result, pages, currentPage, pageResults } = data;

  return (
    <>
      {search != null && (
        <h1 className="mt-12 text-2xl font-semibold leading-[48px] text-gray-700">
          {result.length > 0
            ? `Kết quả tìm kiếm cho`
            : `Không có kết quả tìm kiếm cho`}{" "}
          {`"${search}"`}
        </h1>
      )}
      <ShopProductList
        products={result}
        pages={pages}
        currentPage={currentPage}
        pageSize={pageResults}
      />
    </>
  );
};

const Categories = async () => {
  const res = await fetch(`http://localhost:5000/api/Category`, {
    method: "GET",
  });

  const categories: ApiResponse<ICategory[]> = await res.json();
  const { data, success, message } = categories;
  // console.log(data);

  return <CategorySidebar categories={data} />;
};

const ProductPage = ({
  searchParams,
}: {
  searchParams: { search?: string; page?: number };
}) => {
  const { search, page } = searchParams;
  return (
    <div className="mt-12 px-4 md:px-8 lg:px-16 xl:px-32 2xl:px-64 relative">
      {/* CAMPAIGN */}
      <div className="hidden bg-pink-50 px-4 sm:flex justify-between h-64">
        <ProductBanner />
      </div>
      <div className="flex">
        <div className="hidden md:block md:basis-[18%]">
          <Suspense fallback={<Loading />}>
            <Categories />
          </Suspense>
        </div>
        <div className="ml-4 basis-full md:basis-[82%]">
          <Suspense fallback={<Loading />}>
            <Products search={search || null} page={page || undefined} />
          </Suspense>
        </div>
      </div>
    </div>
  );
};

export default ProductPage;
