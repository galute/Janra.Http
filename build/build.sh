#!/bin/bash
error() {
  echo ">>>>>> Failed to build <<<<<<<<<"
  echo ""

  exit 1
}

cleanup() {
  cd $CURRENT_DIR

  # If we're running on the build agent (assume user is ubuntu), docker creates everything as root so
  # change ownership now so that next time build runs it can clean up
  if [ "$USER" = "ubuntu" ]; then
  	  sudo chown -R ubuntu:ubuntu build
  fi
}

trap error ERR
trap cleanup EXIT

CURRENT_DIR=`pwd`

if [ -z "$BUILD_NUMBER" ]; then
    export BUILD_NUMBER=0.1
fi


VERSION=`cat ../version/VERSION`
HOST=`hostname`
TIMESTAMP=`date`

echo
echo ===========================================================
echo building Janra.Http, VERSION = $VERSION.$BUILD_NUMBER
echo ===========================================================
echo

echo
echo -n "Writing AssemblyInfo.cs file....."

rm -Rf ./src/Version/AssemblyInfo.cs

cat  > ./src/Version/AssemblyInfo.cs << EOL
using System;
using System.Reflection;

[assembly: AssemblyVersion("$VERSION.$BUILD_NUMBER")]
[assembly: AssemblyFileVersion("$VERSION.$BUILD_NUMBER")]

[assembly: AssemblyInformationalVersion("$VERSION.$BUILD_NUMBER")]
[assembly: AssemblyConfiguration("Built at $TIMESTAMP on $HOST from $GIT_REP by $USER")]

EOL

echo "done"

CURDIR=`dirname $0`
pushd $CURDIR >/dev/null
ABS_CURDIR=`pwd`
popd >/dev/null

docker run --rm \
           -v "$ABS_CURDIR:/build" \
           --workdir /build \
           mono:4.2.3 bash -c ./compile.sh
