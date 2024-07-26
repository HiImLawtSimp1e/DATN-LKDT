import Image from "next/image";

const ProductBanner = () => {
  return (
    <>
      <div className="w-2/3 flex flex-col items-center justify-center gap-8">
        <h1 className="text-4xl font-semibold leading-[48px] text-gray-700">
          Giảm giá tới 50%
          <br />
          Cho mọi sản phẩm
        </h1>
      </div>
      <div className="relative w-1/3">
        <Image src="/woman.png" alt="" fill className="object-contain" />
      </div>
    </>
  );
};

export default ProductBanner;
