//
// Models.fs
//
// Author: Jeremy Clough <jeremy@talesin.net>
//
// Copyright (c) 2017 Jeremy Clough
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace TrackMyWalks.FS

open System

type WalkEntry = {
        Title:      string
        Notes:      string
        Latitude:   double
        Longitude:  double
        Kilometres: double
        Difficulty: string
        Distance:   double
        ImageUrl:   Uri
    }
